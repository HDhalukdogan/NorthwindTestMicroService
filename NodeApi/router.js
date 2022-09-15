const SampleController = require('./controllers/samplesController')

module.exports = function (app) {
    app.post('/api/save', SampleController.createSample);
    app.get('/api/getAll', SampleController.getAllSamples);
    app.get('/api/getById/:id', SampleController.getSample);
    app.delete('/api/delete/:id', SampleController.deleteSample);
    app.put('/api/update', SampleController.updateSample);
}