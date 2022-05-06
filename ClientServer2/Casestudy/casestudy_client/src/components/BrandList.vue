<template>
	<q-page>
		<div class="text-center q-mt-lg">
			<div class="text-h3">Brands</div>
			<div class="text-positive q-mt-lg">{{ state.status }}</div>
			<q-select
				class="q-mt-lg q-ml-lg"
				v-if="state.brands.length > 0"
				style="width: 50vw; margin-bottom: 4vh; background-color: #fff"
				:option-value="'id'"
				:option-label="'name'"
				model-value=""
				:options="state.brands"
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
			brands: [],
		});
		const loadCategories = async () => {
			try {
				state.status = "loading categories...";
				const payload = await fetcher(`Brand`);
				if (payload.error === undefined) {
					state.brands = payload;
					state.status = `loaded ${state.brands.length} categoriess`;
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
