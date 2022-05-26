<template>
	<div class="text-center">
		<div class="text-h4 q-mt-md">Tray Contents</div>
		<q-avatar class="q-mt-md" size="xl" square>
			<img :src="`tray.png`" />
		</q-avatar>
		<div class="text-h6 text-positive">{{ state.status }}</div>
	</div>
	<div
		v-if="state.tray.length > 0"
		style="width: 90vw; margin-left: 5vw; margin-top: 1vh"
	>
		<div>
			<q-item style="bottom: -2vh">
				<q-item-section class="col-2 q-ml-sm text-h6 text-primary">
					QTY
				</q-item-section>
				<q-item-section class="q-ml-sm text-h6 text-primary">
					DESCRIPTION
				</q-item-section>
			</q-item>
			<q-scroll-area style="height: 40vh">
				<q-card class="q-ma-md">
					<q-list separator>
						<q-item v-for="item in state.tray" :key="item.id" clickable>
							<q-item-section class="col-2">
								{{ item.qty }}
							</q-item-section>
							<q-item-section class="text-left">
								{{ item.item.description }}
							</q-item-section>
						</q-item>
					</q-list>
				</q-card>
			</q-scroll-area>
		</div>

		<div class="text-center q-my-md">
			<div class="col-4 q-mr-sm">
				<q-btn
					color="primary"
					label="Clear Tray"
					:disable="state.tray.length < 0"
					@click="clearTray()"
				/>
			</div>
		</div>

		<div class="text-h6 text-center text-primary">Nutritional Totals</div>
		<table style="border: solid; margin-top: 1vh">
			<tr>
				<td style="width: 20%; font-weight: bold">Protein</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.proTot }}
				</td>
				<td style="width: 20%; font-weight: bold">Calories</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.calTot }}
				</td>
				<td style="width: 20%; font-weight: bold">Carbs.</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.carbTot }}
				</td>
			</tr>
			<tr>
				<td style="width: 20%; font-weight: bold">Fibre</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.fbrTot }}
				</td>
				<td style="width: 20%; font-weight: bold">Choles.</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.cholTot }}
				</td>
				<td style="width: 20%; font-weight: bold">Salt</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.saltTot }}
				</td>
			</tr>
			<tr>
				<td style="width: 20%; font-weight: bold">Fat</td>
				<td style="width: 10%; text-align: right; padding-right: 3vw">
					{{ state.fatTot }}
				</td>
				<td colspan="4">&nbsp;</td>
			</tr>
		</table>
	</div>
</template>
<script>
import { reactive, onMounted } from "vue";

export default {
	setup() {
		onMounted(() => {
			if (sessionStorage.getItem("tray")) {
				state.tray = JSON.parse(sessionStorage.getItem("tray"));
				state.tray.forEach((trayItem) => {
					state.fbrTot += trayItem.item.fibre * trayItem.qty;
					state.calTot += trayItem.item.calories * trayItem.qty;
					state.saltTot += trayItem.item.salt * trayItem.qty;
					state.fatTot += trayItem.item.fat * trayItem.qty;
					state.cholTot += trayItem.item.cholesterol * trayItem.qty;
					state.proTot += trayItem.item.protein * trayItem.qty;
					state.carbTot += trayItem.item.carbs * trayItem.qty;
				});
			} else {
				state.tray = [];
			}
		});

		let state = reactive({
			status: "",
			fbrTot: 0,
			calTot: 0,
			saltTot: 0,
			fatTot: 0,
			cholTot: 0,
			proTot: 0,
			carbTot: 0,
			tray: [],
		});

		const clearTray = () => {
			sessionStorage.removeItem("tray");
			state.tray = [];
			state.status = "tray cleared";
		};

		return {
			state,
			clearTray,
		};
	},
};
</script>
