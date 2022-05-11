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
						<q-item v-for="item in state.menuitems" :key="item.id">
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

		return {
			state,
			loadMenuitems,
		};
	},
};
</script>
