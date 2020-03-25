// ==UserScript==
// @name         Derdini sikeyim butonu
// @namespace    tbelbek.com
// @require http://code.jquery.com/jquery-3.4.1.min.js
// @version      0.1
// @description  Entrylere kişisel tatmin için bir derdini cima eyleyeyim butonu ekler.
// @author       Tughan Belbek
// @match        *eksisozluk.com/*
// @grant        none
// @copyright 2020, tbelbek (Author Website)
// @license MIT
// @updateURL https://openuserjs.org/meta/tbelbek/My_Script.meta.js
// ==/UserScript==

(function () {
    'use strict';

    $("#entry-item-list > li").each(function (i, o) {
        var entryId = $(o).data("id");
        console.log(entryId);
        $(o).find("div.feedback").append('<button type="button" class="primary derdini-sikeyim" data-id='+entryId+' style="padding:0px 2px 0px 2px;">derdini sikeyim</button>');
    });

    var i = 0;
    $(".derdini-sikeyim").click(function () {
        var entryId = $(this).data("id");
        $.get("https://tbelbek.com/ds/derdini/sikeyim?entryId=" + entryId, function (data) {
            alert("Derdiniz " + data + " kez sikilmiştir.");
        });
    });
})();