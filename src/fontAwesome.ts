import type { App } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faArrowUpRightFromSquare,
  faBan,
  faChevronLeft,
  faCircleInfo,
  faDollarSign,
  faGifts,
  faHome,
  faKey,
  faPaperPlane,
  faPlus,
  faRobot,
  faTimes,
  faTrash,
  faTriangleExclamation,
  faUser,
} from "@fortawesome/free-solid-svg-icons";

library.add(
  faArrowUpRightFromSquare,
  faBan,
  faChevronLeft,
  faCircleInfo,
  faDollarSign,
  faGifts,
  faHome,
  faKey,
  faPaperPlane,
  faPlus,
  faRobot,
  faTimes,
  faTrash,
  faTriangleExclamation,
  faUser
);

export default function (app: App) {
  app.component("font-awesome-icon", FontAwesomeIcon);
}
