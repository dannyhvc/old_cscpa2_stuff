<template>
	<!-- page logo -->
	<div class="text-center">
		<div class="text-h3">Car Case</div>

		<q-avatar class="q-mb-md" size="xl" square>
			<img :src="`/img/CompanyLogo.png`" />
		</q-avatar>
	</div>

	<div class="text-center">
		<div class="text-h4 q-mt-md">Cart Contents</div>
		<q-avatar class="q-mt-md" size="xl" square>
			<!-- TODO add shopping cart image-->
			<img :src="`img/cart.svg`" />
		</q-avatar>
		<div class="text-positive q-mt-lg">{{ state.status }}</div>
	</div>

	<div
		v-if="state.cart.length > 0"
		style="width: 90vw; margin-left: 5vw; margin-top: 1vh"
	>
		<div>
			<q-scroll-area style="height: 50vh">
				<q-card class="q-ma-md">
					<q-list separator>
						<!-- list menu titles -->
						<q-item style="bottom: -1vh">
							<q-item-section class="text-left">
								<strong>Name</strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong>Qty</strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong>MSRP</strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong>Extended</strong>
							</q-item-section>
						</q-item>

						<!-- cart listings -->
						<q-item v-for="item in state.cart" :key="item.id" clickable>
							<q-item-section class="text-left">
								{{ item.id }}
							</q-item-section>
							<q-item-section class="text-left">
								{{ item.qty }}
							</q-item-section>
							<q-item-section class="text-left">
								{{ formatCurrency(item.item.msrp) }}
							</q-item-section>
							<q-item-section class="text-left">
								{{ formatCurrency(item.item.msrp) }}
							</q-item-section>
						</q-item>

						<!-- sub, tax, and total display in list-->
						<q-item>
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left">
								<strong>Sub: </strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong>
									{{ formatCurrency(state.subTotal) }}
								</strong>
							</q-item-section>
						</q-item>
						<q-item>
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left">
								<strong>Tax({{ state.tax * 100 }}%): </strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong>
									{{ formatCurrency(state.taxCost) }}
								</strong>
							</q-item-section>
						</q-item>
						<q-item class="justify-content-end">
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left"> </q-item-section>
							<q-item-section class="text-left">
								<strong class="text-primary">Total: </strong>
							</q-item-section>
							<q-item-section class="text-left">
								<strong class="text-primary">
									{{ formatCurrency(state.total) }}
								</strong>
							</q-item-section>
						</q-item>
					</q-list>
				</q-card>
			</q-scroll-area>
			<!-- clear cart button -->
			<div class="col-3 text-center">
				<q-btn
					class="q-mr-sm"
					color="primary"
					label="Save Cart"
					:disable="state.cart.length < 1"
					@click="saveCart()"
				/>
				<q-btn
					color="primary"
					label="Empty Cart"
					:disable="state.cart.length < 1"
					@click="clearCart()"
				/>
			</div>
		</div>
	</div>
</template>
<script>
import { onMounted, reactive } from "vue";
import { formatCurrency } from "../utils/formatutils";
import { poster } from "../utils/apiutil";

export default {
	setup() {
		let state = reactive({
			status: "",
			total: 0.0,
			subTotal: 0.0,
			taxCost: 0.0,
			tax: 0.13,
			cart: [],
		});

		onMounted(() => {
			if (sessionStorage.getItem("cart")) {
				state.cart = JSON.parse(sessionStorage.getItem("cart"));
				state.cart.forEach((cartItem) => {
					state.subTotal += cartItem.item.msrp * cartItem.qty;
				});
				state.taxCost = state.subTotal * state.tax;
				state.total = state.subTotal * (1 + state.tax);
			} else {
				state.cart = [];
			}
		});

		const clearCart = () => {
			sessionStorage.removeItem("cart");
			state.cart = [];
			state.status = "cart cleared";
		};

		const saveCart = async () => {
			let customer = JSON.parse(sessionStorage.getItem("user"));
			let cart = JSON.parse(sessionStorage.getItem("cart"));
			try {
				state.status = "sending cart info to server";
				let orderHelper = { email: customer.email, items: cart };
				let payload = await poster("Order", orderHelper);

				if (payload.indexOf("not") > 0) {
					state.status = payload;
				} else {
					clearCart();
					state.status = payload;
				}
			} catch (err) {
				console.log(err);
				state.status = `Error add cart: ${err}`;
			}
		};

		return {
			state,
			formatCurrency,
			clearCart,
			saveCart,
		};
	},
};
</script>
