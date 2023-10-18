import { createRouter, createWebHistory } from "vue-router";
import HomeView from "./views/HomeView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      name: "Home",
      path: "/",
      component: HomeView,
    },
    // Wishes
    {
      name: "WishlistRoot",
      path: "/wishlist",
      redirect: { name: "Home" },
    },
    {
      name: "WishlistView",
      path: "/wishlist/:id",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("./views/WishlistView.vue"),
    },
    {
      name: "WishView",
      path: "/wishlist/:listId/:itemId",
      component: () => import("./views/WishView.vue"),
    },
    // NotFound
    {
      name: "NotFound",
      path: "/:pathMatch(.*)*",
      component: () => import("./views/NotFound.vue"),
    },
  ],
});

export default router;
