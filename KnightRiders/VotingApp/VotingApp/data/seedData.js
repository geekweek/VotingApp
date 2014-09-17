(function (seedData) {

    seedData.initialNotes = [{
        name: "History",
        notes: [{
            note : "1st note",
            color: "yellow",
            author: "nani"
        },{
            note : "1st note",
            color: "yellow",
            author: "nani"
        }]
    },{
        name: "People",
        notes: [{
            note : "1st note",
            color: "yellow",
            author: "nani"
        },{
            note : "1st note",
            color: "yellow",
            author: "nani"
        }]
    }]


    seedData.initialTopics = [{
        name: "Design Patterns",
        presenter: "Pravinth",
        Date: "20th July 2014",
        votes: [{
            rating : "7",
            comments: "Testing the comments",
            author: "nani"
        },{
            rating : "8",
            comments: "Testing the comments by other user",
            author: "uday"
        }]
    },{
        name: "JavaScript",
        presenter: "Uday",
        Date: "13th July 2014",
        votes: [{
            rating : "4",
            comments: "Testing the comments",
            author: "nani"
        },{
            rating : "9",
            comments: "Testing the comments by other user",
            author: "uday"
        }]
    }]
})(module.exports);