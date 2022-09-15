const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const sampleSchema = new Schema({
    sampleName: String
});

const ModelClass = mongoose.model('sample', sampleSchema);

module.exports = ModelClass;