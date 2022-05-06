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
				model-value=""
				:options="state.categories"
				label="Select a Category"
			/>
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
		return {
			state,
		};
	},
};
</script>
