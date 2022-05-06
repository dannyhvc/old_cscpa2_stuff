const routes = [
	{
		path: "/",
		component: () => import("layouts/MainLayout.vue"),
		children: [
			// Home Page
			{
				path: "/",
				name: "home",
				component: () => import("pages/HomePage.vue"),
			},
			// DataUtil
			{
				path: "/datautil",
				name: "datautil",
				component: () => import("components/DataUtil.vue"),
			},
			// Categories
			{
				path: "/brands",
				name: "brands",
				component: () => import("components/BrandList.vue"),
			},
		],
	},
	// Always leave this as last one,
	// but you can also remove it
	{
		path: "/:catchAll(.*)*",
		component: () => import("pages/ErrorNotFound.vue"),
	},
];
export default routes;
