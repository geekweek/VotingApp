(function (controllers) {
    
    var homeController = require("./homeController.js");
    var notesController = require("./notesController.js");
    var topicsController = require("./topicsController.js");

    controllers.init = function (app) {
        homeController.init(app);
        notesController.init(app);
        topicsController.init(app);
    };
})(module.exports);