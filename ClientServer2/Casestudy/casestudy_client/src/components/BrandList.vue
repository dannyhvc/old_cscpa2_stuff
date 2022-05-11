<template>
	<q-page>
		<div class="text-center q-mt-lg">
			<div class="text-h3">Car Case</div>

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
			<q-scroll-area style="height: 55vh">
				<q-card class="q-ma-md">
					<q-list separator>
						<q-item v-for="product in state.products" :key="product.id">
							<q-item-section avatar>
								<q-avatar>
									<!-- for adding picture next to car brand -->
									<img :src="`/img/${product.graphicName}`" />
								</q-avatar>
							</q-item-section>
							<q-item-section class="text-left">
								{{ product.description }}
							</q-item-section>
						</q-item>
					</q-list>
				</q-card>
			</q-scroll-area>
		</div>
	</q-page>
</template>
<script>
import { reactive, onMounted } from "vue";
import { fetcher } from "../utils/apputil";
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
		});

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
				console.log(state);
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		return {
			state,
			loadProducts,
		};
	},
};
</script>
