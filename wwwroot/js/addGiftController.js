(function () {
    "use strict";

    angular.module("app-gifts")
        .controller("addGiftController", addGiftController);

    function addGiftController($http) {
        var vm = this;
        vm.newGift = {};
        vm.holidays = [];
        vm.recipients = [];
        vm.errorMessage = "";
        vm.isBusy = true;

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

        vm.addGift = function () {
            vm.isBusy = true;
            var newGifts = [vm.newGift];
            $http.post("/api/gifts", newGifts)
            .then(function (response) {
                vm.newGift = {};
            },
            function (error) {
                vm.errorMessage = "Failed to add gift";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        }

    }

})();