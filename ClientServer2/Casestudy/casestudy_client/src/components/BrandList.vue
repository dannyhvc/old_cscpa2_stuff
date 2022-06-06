<template>
	<q-page>
		<div class="text-center q-mt-lg">
			<!-- page title -->
			<div class="text-h3">Car Case</div>

			<!-- page logo -->
			<q-avatar class="q-mb-md" size="xl" square>
				<img :src="`/img/CompanyLogo.png`" />
			</q-avatar>

			<div class="text-positive q-mt-lg">{{ state.status }}</div>
			<q-select
				class="q-mt-lg q-ml-lg"
				v-if="state.brands.length > 0"
				style="width: 50vw; margin-bottom: 4vh; background-color: #fff"
				:option-value="'id'"
				:option-label="'name'"
				v-model="state.selectedBrandId"
				:options="state.brands"
				label="Select a Brand"
				emit-value
				map-options
				@update:model-value="loadProducts()"
			/>
			<div
				class="text-h6 text-bold text-center text-primary"
				v-if="state.products.length > 0"
			>
				{{ state.selectedBrand.name }} products
			</div>
			<!-- listed products -->
			<q-scroll-area style="height: 55vh">
				<q-card class="q-ma-md">
					<q-list separator>
						<q-item
							v-for="product in state.products"
							:key="product.id"
							clickable
							@click="selectProduct(product.id)"
						>
							<q-item-section avatar>
								<q-avatar>
									<!-- for adding picture next to car brand -->
									<img :src="`/img/${product.graphicName}`" />
								</q-avatar>
							</q-item-section>
							<q-item-section class="text-left">
								{{ product.id }}
							</q-item-section>
						</q-item>
					</q-list>
				</q-card>
			</q-scroll-area>
		</div>

		<!-- product details and dialog modal -->
		<q-dialog
			v-model="state.itemSelected"
			transition-show="rotate"
			transition-hide="rotate"
		>
			<q-card>
				<q-card-actions align="right">
					<q-btn flat label="X" color="primary" v-close-popup class="text-h5" />
				</q-card-actions>
				<q-card-section class="q-pa-none text-center">
					<!-- /img/${product.graphicName} -->
					<img
						:src="`/img/${state.selectedProduct.graphicName}`"
						class="item-image"
					/>
				</q-card-section>
				<q-card-section class="q-pa-none text-center">
					<div
						style="
							font-weight: bold;
							font-size: larger;
							margin-top: 3vh;
							text-align: center;
						"
					>
						<div
							class="text-h5 text-bold text-center text-primary"
							v-if="state.products.length > 0"
						>
							<!-- selected product - formate local msrp -->
							{{ state.selectedProduct.productName }} -
							{{ formatCurrency(state.selectedProduct.msrp) }}
						</div>
					</div>
					<div style="font-size: larger; margin-top: 3vh; text-align: center">
						{{ state.selectedProduct.description }}
					</div>
				</q-card-section>
				<q-card-section>
					<div class="row">
						<div class="col-2 q-mr-sm">
							<q-input
								v-model.number="state.qty"
								type="number"
								filled
								style="max-width: 15vw"
								placeholder="qty"
								hint="Qty"
								dense
							/>
						</div>
					</div>
					<!-- add to cart submit button -->

					<div class="col-4 q-mr-sm">
						<q-btn
							color="primary"
							label="Add To Cart"
							:disable="state.qty < 0"
							@click="addToCart()"
						/>
					</div>
					<div class="col-4">
						<q-btn
							color="primary"
							label="View Cart"
							:disable="state.cart.length < 1"
							@click="viewCart()"
						/>
					</div>
				</q-card-section>

				<q-card-section class="text-center text-positive">
					{{ state.dialogStatus }}
				</q-card-section>
			</q-card>
		</q-dialog>
	</q-page>
</template>
<script>
import { reactive, onMounted } from "vue";
import { fetcher } from "../utils/apiutil";
import { formatCurrency } from "../utils/formatutils";
import { useRouter } from "vue-router";

export default {
	setup() {
		onMounted(() => {
			loadBrands();
		});

		let state = reactive({
			status: "",
			brands: [],
			products: [],
			selectedBrand: {},
			selectedBrandId: "",
			selectedProduct: {},
			dialogStatus: "",
			itemSelected: false,
			cart: [],
			qty: 0,
		});

		const router = useRouter();

		const loadBrands = async () => {
			try {
				state.status = "loading brands...";
				const payload = await fetcher(`Brand`);
				if (payload.error === undefined) {
					state.brands = payload;
					state.status = `loaded ${state.brands.length} brands`;
				} else {
					state.status = payload.error;
				}
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const loadProducts = async () => {
			try {
				state.selectedBrand = state.brands.find(
					(brand) => brand.id === state.selectedBrandId
				);
				state.status = `finding products for brand ${state.selectedBrand}...`;
				state.products = await fetcher(`Product/${state.selectedBrand.id}`);
				state.status = `loaded ${state.products.length} products for ${state.selectedBrand.name}`;
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const selectProduct = async (menuitemid) => {
			try {
				// q-item click sends us the id, so we need to find the object
				state.selectedProduct = state.products.find(
					(item) => item.id === menuitemid
				);
				state.selectedProduct.brand = state.selectedBrand;
				state.itemSelected = true;
				state.dialogStatus = "";
				if (sessionStorage.getItem("cart")) {
					state.cart = JSON.parse(sessionStorage.getItem("cart"));
				}
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const addToCart = () => {
			let index = -1;
			if (state.cart.length > 0) {
				index = state.cart.findIndex(
					// is item already on the cart
					(item) => item.id === state.selectedProduct.id
				);
			}
			if (state.qty > 0) {
				index === -1 // add
					? state.cart.push({
							id: state.selectedProduct.id,
							qty: state.qty,
							item: state.selectedProduct,
					  })
					: (state.cart[index] = {
							// replace
							id: state.selectedProduct.id,
							qty: state.qty,
							item: state.selectedProduct,
					  });
				state.dialogStatus = `${state.qty} item(s) added`;
			} else {
				index === -1 ? null : state.cart.splice(index, 1); // remove
				state.dialogStatus = `item(s) removed`;
			}
			sessionStorage.setItem("cart", JSON.stringify(state.cart));
			state.qty = 0;
		};

		const viewCart = () => {
			router.push("cart");
		};

		return {
			state,
			loadProducts,
			selectProduct,
			addToCart,
			formatCurrency,
			viewCart,
		};
	},
};
</script>
