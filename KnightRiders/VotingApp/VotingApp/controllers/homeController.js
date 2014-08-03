(function (homeController) {
    var data = require("../data");
    var auth = require("../auth");
    homeController.init = function (app) {
        
        app.get("/", function (req, res) {
            data.getTopics(function (err, results) {
                res.render("topics", {
                    title: "List of Topics", 
                    error: err, 
                    topics: results,
                    newTopicError : req.flash("newTopicErr"),
                    user: req.user
                });
            });
        });
        
        app.get("/notes/:categoryName", 
            auth.ensureAuthenticated,
            function (req, res) {
            var categoryName = req.params.categoryName;
            res.render("notes", { title: categoryName, user: req.user });
        });
        
        app.post("/newCategory", function (req, res) {
            var categoryName = req.body.categoryName;
            data.createNewCategory(categoryName, function (err) {
                if (err) {
                    req.flash("newCatErr", err);
                    res.redirect("/");
                } else {
                    res.redirect("/notes" + categoryName);
                }
            });
        });
        

        app.get("/topics", 
            auth.ensureAuthenticated,
            function (req, res) {
            data.getTopics(function (err, results) {
                    res.render("topics", {
                        title: "List of Topics", 
                        error: err, 
                        topics: results,
                        newTopicError : req.flash("newTopicErr"),
                        user: req.user
                    });
                });
        });
        
        app.get("/topics/:topicName", 
            auth.ensureAuthenticated,
            function (req, res) {
            var topicName = req.params.topicName;
            res.render("topicDetails", { title: topicName, user: req.user });
        });

    };
})(module.exports);