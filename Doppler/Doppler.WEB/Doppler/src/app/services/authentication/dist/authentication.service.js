"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.AuthenticationService = void 0;
var core_1 = require("@angular/core");
var UrlResolver_1 = require("../../../environments/UrlResolver");
var static_repository_1 = require("../../../repository/static.repository");
var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(http) {
        this.http = http;
        this.staticRepository = new static_repository_1.StaticRepository();
    }
    AuthenticationService.prototype.login = function (UserName, Password) {
        var _this = this;
        if (UserName && Password) {
            this.http.post(UrlResolver_1.UrlResolver.GetLoginUrl(), {
                'UserName': UserName,
                'Password': Password
            }).subscribe(function (authResult) {
                if (authResult) {
                    _this.staticRepository.saveLoginData(authResult);
                }
            });
        }
    };
    AuthenticationService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], AuthenticationService);
    return AuthenticationService;
}());
exports.AuthenticationService = AuthenticationService;
