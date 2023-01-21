import React, { useCallback } from "react";

import {
    FirebaseCMSApp
} from "firecms";

import "typeface-rubik";
import "@fontsource/ibm-plex-mono";

import { productsCollection } from "./collections/productCollection";

// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCN-oeB9SPkZD57r7c95enUaXUmD10KJTQ",
  authDomain: "the-practical-shooter-app.firebaseapp.com",
  projectId: "the-practical-shooter-app",
  storageBucket: "the-practical-shooter-app.appspot.com",
  messagingSenderId: "175640954305",
  appId: "1:175640954305:web:0c1eafaa3f2cc6d1f39b40",
  measurementId: "G-T7ZVCSDN5G"
};

export default function App() {

    const myAuthenticator = useCallback(async ({
                                                                    user,
                                                                    authController
                                                                }) => {

        if (user?.email?.includes("flanders")) {
            throw Error("Stupid Flanders!");
        }

        console.log("Allowing access to", user?.email);
        // This is an example of retrieving async data related to the user
        // and storing it in the user extra field.
        const sampleUserRoles = await Promise.resolve(["admin"]);
        authController.setExtra(sampleUserRoles);

        return true;
    }, []);

    return <FirebaseCMSApp
        name={"The Practical Shooter CMS"}
        authentication={myAuthenticator}
        collections={[productsCollection]}
        firebaseConfig={firebaseConfig}
    />;
}