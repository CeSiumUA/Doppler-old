"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.ToolbarComponent = void 0;
var core_1 = require("@angular/core");
var ToolbarComponent = /** @class */ (function () {
    function ToolbarComponent(authService) {
        this.authService = authService;
    }
    Object.defineProperty(ToolbarComponent.prototype, "userName", {
        get: function () {
            return this.authService.userName;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ToolbarComponent.prototype, "phoneNumber", {
        get: function () {
            return this.authService.phoneNumber;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ToolbarComponent.prototype, "iconUrl", {
        get: function () {
            return (this.authService.profilePicture) ? this.authService.profilePicture : 'assets/icons/profile_picture.png';
        },
        enumerable: false,
        configurable: true
    });
    ToolbarComponent.prototype.clearLoginData = function () {
        this.authService.logout();
    };
    ToolbarComponent = __decorate([
        core_1.Component({
            selector: 'app-toolbar',
            templateUrl: './toolbar.component.html',
            styleUrls: ['./toolbar.component.css']
        })
    ], ToolbarComponent);
    return ToolbarComponent;
}());
exports.ToolbarComponent = ToolbarComponent;