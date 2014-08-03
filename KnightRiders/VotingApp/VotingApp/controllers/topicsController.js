(function (topicsController) {
    var data = require("../data");
    
    topicsController.init = function (app) {
        app.get("/topics/update/:topicName", function (req, res) {

            res.render("updateTopic", {
                title: "Update Existing Presentation Topic", 
                user : req.user
            });
        });
        
        app.post("/api/topics/update/:topicName", function (req, res) {
            var topicName = req.params.topicName;
            var topicToUpdate = {
                name: req.body.name,
                presenter : req.body.presenter,
                Date : req.body.Date,
                Description : req.body.Description
            };

            data.updateTopic(topicName, topicToUpdate, function (err) {
                if (err) {
                    console.log("Unable to update");
                } else {
                    res.writeHead(302, {
                        'Location': '/'
                    });
                    res.end();
                    //res.redirect("/");
                }
            });
        });

        app.get("/newTopic", function (req, res) {
            res.render("newTopic", { title: "Create New Presentation Topic", user : req.user });
        });
        
        app.post("/newTopic", function (req, res) {
            var topicToInsert = {
                name : req.body.name,
                presenter: req.body.username,
                Date: req.body.Date,
                Description: req.body.Description,
                votes:[]
            };
            
            
            data.createNewTopic(topicToInsert, function (err) {
                if (err) {
                    next(400, "Failed to add new topic to datasource :" + err);
                } else {
                    res.redirect("/topics/" + topicToInsert.name);
                }
            });
        });

        app.get("/api/topics/:topicName", function (req, res) {
            var topicName = req.params.topicName;
            data.getTopicDetails(topicName, function (err, topic) {
                if (err) {
                    next(400, err);
                } else {
                    res.set("Content-Type", "application/json");
                    res.send(topic);
                }
            });
        });
        
        app.get("/api/topicRating/:topicName", function (req, res) {
            var topicName = req.params.topicName;
            var username = req.user.name;
            data.getTopicDetails(topicName, function (err, topic) {
                if (err) {
                    next(400, err);
                } else {
                    var userRating = null;
                    var votes = topic.votes;//.toArray();
                    if (votes !== null && votes !== undefined) {
                        for (i = 0; i < votes.length; i++) {
                            if (votes[i].author === username) {
                                userRating = {
                                    rating : votes[i].rating,
                                    comments: votes[i].comments,
                                    author: votes[i].author,
                                    IsExisting : true
                                }
                            }
                        }
                    }
                    res.set("Content-Type", "application/json");
                    res.send(userRating);
                }
            });
        });

        app.post("/api/topics/:topicName", function (req, res) {
            var topicName = req.params.topicName;
            var topicRatingToInsert = {
                comments: req.body.comments,
                rating : req.body.rating,
                author: req.body.author,
                IsExisting:req.body.IsExisting
            };
            if (req.user) {
                topicRatingToInsert.author = req.user.name;
            }
           
            if (topicRatingToInsert.IsExisting) {
                data.updateTopicRating(topicName, topicRatingToInsert, function (err) {
                    if (err) {
                        next(400, "Failed to add note to datasource " + err);
                    } else {
                        res.set("Content-Type", "application/json");
                        res.send(201, topicRatingToInsert);
                    }
                });
            } else {
                data.addTopicRating(topicName, topicRatingToInsert, function (err) {
                    if (err) {
                        next(400, "Failed to add note to datasource " + err);
                    } else {
                        res.set("Content-Type", "application/json");
                        res.send(201, topicRatingToInsert);
                    }
                });
            }

        });
    };

})(module.exports);