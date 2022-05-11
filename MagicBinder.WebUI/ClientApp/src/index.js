import "react-app-polyfill/ie11";
import React from "react";
import ReactDOM from "react-dom";
import {BrowserRouter} from "react-router-dom";
import {Index} from "layouts/exports"
import {ScrollOnNavigation} from "components/routing";
import "./assets/vendor/font-awesome";

require("dotenv").config();

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");

ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <ScrollOnNavigation/>
        <Index/>
        <div id="modal-root"/>
    </BrowserRouter>,
    rootElement);

