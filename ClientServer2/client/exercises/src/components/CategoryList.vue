<template>
	<q-page>
		<div class="text-center q-mt-lg">
			<div class="text-h3">Categories</div>
			<div class="text-positive q-mt-lg">{{ state.status }}</div>
			<q-select
				class="q-mt-lg q-ml-lg"
				v-if="state.categories.length > 0"
				style="width: 50vw; margin-bottom: 4vh; background-color: #fff"
				:option-value="'id'"
				:option-label="'name'"
				v-model="state.selectedCategoryId"
				:options="state.categories"
				label="Select a Category"
				emit-value
				map-options
				@update:model-value="loadMenuitems()"
			/>
			<div
				class="text-h6 text-bold text-center text-primary"
				v-if="state.menuitems.length > 0"
			>
				{{ state.selectedCategory.name }} ITEMS
			</div>
			<q-scroll-area style="height: 55vh">
				<q-card class="q-ma-md">
					<q-list separator>
						<q-item
							v-for="item in state.menuitems"
							:key="item.id"
							clickable
							@click="selectMenuItem(item.id)"
						>
							<q-item-section avatar>
								<q-avatar>
									<img :src="`/burger.jpg`" />
								</q-avatar>
							</q-item-section>
							<q-item-section class="text-left">
								{{ item.description }}
							</q-item-section>
						</q-item>
					</q-list>
				</q-card>
			</q-scroll-area>
		</div>
		<q-dialog
			v-model="state.itemSelected"
			transition-show="rotate"
			transition-hide="rotate"
		>
			<q-card>
				<q-card-actions align="right">
					<q-btn flat label="X" color="primary" v-close-popup class="text-h5" />
				</q-card-actions>
				<q-card-section>
					<div class="text-subtitle2 text-center">
						{{ state.selectedMenuItem.description }}
					</div>
				</q-card-section>
				<q-card-section class="q-pa-none text-center">
					<img :src="`/burger.jpg`" class="item-image" />
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
						Nutrional Info
					</div>
					<table style="border: solid; margin: 2vh">
						<tr>
							<td style="width: 20%; font-weight: bold">Protein</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.protein }}
							</td>
							<td style="width: 20%; font-weight: bold">Calories</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.calories }}
							</td>
							<td style="width: 20%; font-weight: bold">Carbs.</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.carbs }}
							</td>
						</tr>
						<tr>
							<td style="width: 20%; font-weight: bold">Fibre</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.fibre }}
							</td>
							<td style="width: 20%; font-weight: bold">Choles.</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.cholesterol }}
							</td>
							<td style="width: 20%; font-weight: bold">Salt</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.salt }}
							</td>
						</tr>
						<tr>
							<td style="width: 20%; font-weight: bold">Fat</td>
							<td style="width: 10%; text-align: right; padding-right: 3vw">
								{{ state.selectedMenuItem.fat }}
							</td>
							<td colspan="4">&nbsp;</td>
						</tr>
					</table>
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
						<div class="col-4 q-mr-sm">
							<q-btn
								color="primary"
								label="Add To Tray"
								:disable="state.qty < 0"
								@click="addToTray()"
							/>
						</div>
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
import { fetcher } from "../utils/apputil";

export default {
	setup() {
		onMounted(() => {
			loadCategories();
		});

		let state = reactive({
			status: "",
			categories: [],
			menuitems: [],
			selectedCategory: {},
			selectedCategoryId: "",
			selectedMenuItem: {},
			dialogStatus: "",
			itemSelected: false,
			qty: 0,
			tray: [],
		});

		const loadCategories = async () => {
			try {
				state.status = "loading categories...";
				const payload = await fetcher(`Category`);
				if (payload.error === undefined) {
					state.categories = payload;
					state.status = `loaded ${state.categories.length} categoriess`;
				} else {
					state.status = payload.error;
				}
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const loadMenuitems = async () => {
			try {
				state.selectedCategory = state.categories.find(
					(category) => category.id === state.selectedCategoryId
				);
				state.status = `finding menuitems for category ${state.selectedCategory}...`;
				state.menuitems = await fetcher(
					`Menuitem/${state.selectedCategory.id}`
				);
				state.status = `loaded ${state.menuitems.length} menu items for ${state.selectedCategory.name}`;
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const selectMenuItem = async (menuitemid) => {
			try {
				// q-item click sends us the id, so we need to find the object
				state.selectedMenuItem = state.menuitems.find(
					(item) => item.id === menuitemid
				);
				state.itemSelected = true;
				state.dialogStatus = "";
				if (sessionStorage.getItem("tray")) {
					state.tray = JSON.parse(sessionStorage.getItem("tray"));
				}
			} catch (err) {
				console.log(err);
				state.status = `Error has occured: ${err.message}`;
			}
		};

		const addToTray = () => {
			let index = -1;
			if (state.tray.length > 0) {
				index = state.tray.findIndex(
					// is item already on the tray
					(item) => item.id === state.selectedMenuItem.id
				);
			}
			if (state.qty > 0) {
				index === -1 // add
					? state.tray.push({
							id: state.selectedMenuItem.id,
							qty: state.qty,
							item: state.selectedMenuItem,
					  })
					: (state.tray[index] = {
							// replace
							id: state.selectedMenuItem.id,
							qty: state.qty,
							item: state.selectedMenuItem,
					  });
				state.dialogStatus = `${state.qty} item(s) added`;
			} else {
				index === -1 ? null : state.tray.splice(index, 1); // remove
				state.dialogStatus = `item(s) removed`;
			}
			sessionStorage.setItem("tray", JSON.stringify(state.tray));
			state.qty = 0;
		};

		return {
			state,
			loadMenuitems,
			selectMenuItem,
			addToTray,
		};
	},
};
</script>
