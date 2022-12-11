const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/swagger",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:44379',
        secure: false
    });

    app.use(appProxy);
};
