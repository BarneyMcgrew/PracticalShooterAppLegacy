import {
    buildCollection
} from "Firecms";

export const userCollection = buildCollection({
    name: "Users",
    singularName: "User",
    group: "Account Management",
    path: "users",
    permissions: ({ authController}) => ({
        edit: true,
        create: false,
        delete: true
    }),
    properties: {
        userId: {
            name: "User Id",
            validation: { required: true },
            dataType: "string"
        },
        email: {
            name: "Email Address",
            validation: { required: false },
            dataType: "string"
        },
        displayName: {
            name: "Display Name",
            validation: { required: true },
            dataType: "string"
        },

    }
})

