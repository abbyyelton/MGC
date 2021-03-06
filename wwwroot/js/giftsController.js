﻿(function () {
    "use strict";

    angular.module("app-gifts")
        .controller("giftsController", giftsController);

    function giftsController($http) {
        var vm = this;
        vm.gifts = [];
        vm.holidays = [];
        vm.recipients = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.editing = false;
        
        $http.get("/api/holidays")
            .then(function (response) {
                angular.copy(response.data, vm.holidays);
            },
            function (error) {
                vm.errorMessage = "Failed to load holidays " + error;
            })
            .finally(function () {
                vm.isBusy = false;

            });

        $http.get("/api/recipients")
           .then(function (response) {
               angular.copy(response.data, vm.recipients);
           },
           function (error) {
               vm.errorMessage = "Failed to load recipients " + error;
           })
           .finally(function () {
               vm.isBusy = false;

           });

        $http.get("/api/gifts")
            .then(function (response) {
                angular.copy(response.data, vm.gifts);
            },
            function(error) {
                vm.errorMessage = "Failed to load gifts " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

       
        vm.deleteGift = function(gift, index)
        {
            vm.isBusy = true;

            $http.delete("/api/gifts", { data: gift, headers: { 'Content-Type': 'application/json' } })
            .then(function (response) {
                vm.gifts.splice(index, 1);
            },
            function (error) {
                vm.errorMessage = "Failed to delete gift";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        }

        vm.saveGifts = function()
        {
            vm.isBusy = true;
            
        }
    }

})();