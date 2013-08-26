﻿/// <reference path="libs/require.js" />
require.config({
    paths: {
        jquery: "libs/jquery-2.0.3",
        rsvp: "libs/rsvp.min",
        httpRequester: "libs/http-requester",
        mustache: "libs/mustache",
    }
});

require(["jquery", "mustache", "app/data-persister", "app/controls",],
    function ($, mustache, data, controls) {

    data.students().then(function (students) {
        var studentTemplateString = $("#students-template").html();

        var template = mustache.compile(studentTemplateString);

        var listView = controls.listView(students);
        var listViewHtml = listView.render(template);

        $("body").append(listViewHtml);

    }, function (err) {
        console.error(err);
    });

    $("body").on('click', ".clickable", function () {
        var id = $(this).attr("id");

        data.getStudentMarks(id).then(function (marks) {
            var studentTemplateString = $("#marks-template").html();

            var template = mustache.compile(studentTemplateString);

            var listView = controls.listView(marks);
            var listViewHtml = listView.render(template);

            $("body").html(listViewHtml);
        });
    })
});