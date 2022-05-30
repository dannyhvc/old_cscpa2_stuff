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
				path: "/categories",
				name: "categories",
				component: () => import("components/CategoryList.vue"),
			},
			{
				path: "/tray",
				name: "tray",
				component: () => import("components/TrayContainer.vue"),
			},
			{
				name: "register",
				path: "/register",
				component: () => import("components/RegisterUser.vue"),
			},
			{
				name: "login",
				path: "/login",
				component: () => import("components/LoginUser.vue"),
			},
			{
				name: "logout",
				path: "/logout",
				component: () => import("components/LogoutUser.vue"),
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
