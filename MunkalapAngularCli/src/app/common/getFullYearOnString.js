//https://stackoverflow.com/questions/9354298/how-do-i-write-an-extension-method-in-javascript
Object.defineProperty(String.prototype, "getFullYear", {
    value: function getFullYear() {
        return this.substring(0,4);
    },
    writeable: true,
    configurable: true
});