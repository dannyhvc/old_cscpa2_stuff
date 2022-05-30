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
			{
				path: "/cart",
				name: "cart",
				component: () => import("components/CartContainer.vue"),
			},
			{
				name: "register",
				path: "/register",
				component: () => import("components/RegisterCustomer.vue"),
			},
			{
				name: "login",
				path: "/login",
				component: () => import("components/LoginCustomer.vue"),
			},
			{
				name: "logout",
				path: "/logout",
				component: () => import("components/LogoutCustomer.vue"),
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
