<template>
	<div class="text-center q-mt-md">
		<div class="text-h4">Branch</div>
		<div class="text-h6">Closest branchs on markers</div>
		<div>
			<q-input
				class="q-ma-lg text-h5"
				placeholder="enter current address"
				id="address"
				v-model="state.address"
			/>
			<br />
		</div>
		<q-btn
			label="Gen Map"
			@click="genMap"
			class="q-mb-md"
			style="width: 30vw"
		/>
		<div
			style="height: 50vh; width: 90vw; margin-left: 5vw; border: solid"
			ref="mapRef"
			v-show="state.showmap === true"
		></div>
	</div>
</template>
<script>
import { ref, reactive } from "vue";
import { fetcher } from "../utils/apiutil";
export default {
	name: "MapEx3",
	setup() {
		const mapRef = ref(null);
		let state = reactive({
			status: "",
			address: "",
			showmap: false,
		});
		const genMap = async () => {
			try {
				state.showmap = true;
				const tt = window.tt;
				let url = `https://api.tomtom.com/search/2/geocode/${state.address}.json?key=S7J7pY1kP00wfNAVdes1auYA70agyAJn`;
				let response = await fetch(url);
				let payload = await response.json();
				let lat = payload.results[0].position.lat;
				let lon = payload.results[0].position.lon;
				let map = tt.map({
					key: "S7J7pY1kP00wfNAVdes1auYA70agyAJn",
					container: mapRef.value,
					source: "vector/1/basic-main",
					center: [lon, lat],
					zoom: 8,
				});
				map.addControl(new tt.FullscreenControl());
				map.addControl(new tt.NavigationControl());

				let threeStores = await fetcher(`Branch/${lat}/${lon}`);
				console.log(threeStores);

				threeStores.forEach((store) => {
					let marker = new tt.Marker()
						.setLngLat([store.longitude, store.latitude])
						.addTo(map);
					let popupOffset = 25;
					let popup = new tt.Popup({ offset: popupOffset });
					popup.setHTML(
						`<div id="popup">Store#: ${store.id}</div><div>${store.street}, ${
							store.city
						}
                        <br/>${store.distance.toFixed(2)} mi</div>`
					);
					marker.setPopup(popup);
				});
			} catch (err) {
				state.status = err.message;
			}
		};
		return {
			mapRef,
			state,
			genMap,
		};
	},
};
</script>
