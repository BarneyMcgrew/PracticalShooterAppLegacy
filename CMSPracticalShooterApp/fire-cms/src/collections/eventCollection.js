import {
    buildCollection
} from "firecms";

export const eventCollection = buildCollection({
    name: "Events",
    singularName: "Event",
    group: "Calendar",
    path: "events",
    permissions: ({ authController }) => ({
        edit: true,
        create: true,
        delete: true
    }),
    properties: {
        eventName: {
            name: "Event Name",
            validation: { required: true },
            dataType: "string"
        },
        eventDescription: {
            name: "Description",
            validation: { required: true },
            dataType: "string",
            columnWidth: 300
        }
    }
})