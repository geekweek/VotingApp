(function (data) {
    var seedData = require("./seedData.js");
    var database = require("./database.js");
    
    data.getTopics = function (next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.find().toArray(function (err, results) {
                    if (err) {
                        next(err, null);
                    } else {
                        next(null, results);
                    }
                });
            }
        });
    };
    
    data.createNewTopic = function (topicToInsert, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.find({ name: topicToInsert.name }).count(function (err, count) {
                    if (err) {
                        next(err);
                    } else {
                        if (count != 0) {
                            next("Topic already exists");
                        } else {
                            
                            db.topics.insert(topicToInsert, function (err) {
                                if (err) {
                                    next(err);
                                } else {
                                    next(null);
                                }
                            });
                        }
                    }
                });
            }
        });
    };

    data.getNoteCategories = function (next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.notes.find().toArray(function (err, results) {
                    if (err) {
                        next(err, null);
                    } else {
                        next(null, results);
                    }
                });
            }
        });  
    };
    
    data.createNewCategory = function (categoryName, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.notes.find({ name: categoryName }).count(function (err, count) {
                    if (err) {
                        next(err);
                    } else {
                        if (count != 0) {
                            next("Category already exists");
                        } else {
                            var cat = {
                                name: categoryName,
                                notes: []
                            };
                            db.notes.insert(cat, function (err) {
                                if (err) {
                                    next(err);
                                } else {
                                    next(null);
                                }
                            });
                        }
                    }
                });
            }
        });
    };
    
    data.getNotes = function (categoryName, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.notes.findOne({ name: categoryName }, next);
            }
        });
    };
    
    data.addNote = function (categoryName,noteToInsert, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.notes.update({ name: categoryName }, { $push: { notes: noteToInsert } }, next);                
            }
        });
    };
    
    data.updateTopic = function (topicName, topicToUpdate, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.update({ name: topicName }, {
                    $set: {
                        name: topicToUpdate.name,
                        presenter: topicToUpdate.presenter,
                        Date: topicToUpdate.Date,
                        Description: topicToUpdate.Description
                    }
                }, next);
            }
        });
    };
    
    data.getTopicDetails = function (topicName, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.findOne({ name: topicName }, next);
            }
        });
    };
    
    data.getTopicRatingDetailsByUser = function (topicName,username, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.findOne({ name: topicName, "votes.author" : username}, next);
            }
        });
    };
    
    data.addTopicRating = function (topicName, topicRatingToInsert, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.update({ name: topicName }, { $push: { votes: topicRatingToInsert } }, next);
            }
        });
    };
    
    data.updateTopicRating = function (topicName, topicRatingToInsert, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.topics.update({ name: topicName , "votes.author" : topicRatingToInsert.author }, 
                    {
                    $set: {
                        "votes.$.rating": topicRatingToInsert.rating,
                        "votes.$.comments": topicRatingToInsert.comments
                    }
                }, next);
            }
        });
    };
	

    data.addUser = function (user, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err);
            } else {
                db.users.insert(user, next);
            }
        });
    };
    
    data.getUser = function (username, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err);
            } else {
                db.users.findOne({ username: username }, next);
            }
        });
    };

    function seedDatabase() {
        database.getDb(function (err, db) {
            if (err) {
                console.log("Failed to seed database" + err);
            } else {
                db.notes.count(function (err, count) {
                    if (err) {
                        console.log("Failed to retrieve database count" + err);
                    } else {
                        if (count == 0) {
                            console.log("Seeding the database");
                            seedData.initialNotes.forEach(function (item) {
                                db.notes.insert(item, function (err) {
                                    if (err) {
                                        console.log("Failed to insert into database" + err);
                                    }
                                });
                            });
                            seedData.initialTopics.forEach(function (item) {
                                db.topics.insert(item, function (err) {
                                    if (err) {
                                        console.log("Failed to insert into database" + err);
                                    }
                                });
                            });
                            console.log("Seeding Completed");
                        } else {
                            console.log("Database already seeded"); 
                        }
                    }

                });
            }
        });
    }
    

    seedDatabase();
})(module.exports);