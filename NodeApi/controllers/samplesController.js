const Sample = require('../models/sample');

exports.createSample = function (req, res, next) {
    const sampleName = req.body.sampleName;
    if (!sampleName) {
        return res.status(422).send({ error: 'You must provide name' });
    }

    const sample = new Sample({ sampleName: sampleName })
    sample.save(function (err) {
        if (err) { return next(err); }
    })
    //res.json({sample})
    res.send(sample)
}
exports.getAllSamples = function (req, res, next) {
    Sample.find(function (err, data) {
        if (err) {
            return next(err);
        }
        else {
            res.send(data);
        }
    })

    // const samples = await Sample.find();
    // return res.json(samples)
}
exports.getSample = function (req, res, next) {
    Sample.findOne({ _id: req.params.id }, function (err, data) {
        if (err) { return next(err) }

        res.send(data)

    })
}
exports.deleteSample = function (req, res, next) {
    Sample.remove({ _id: req.params.id }, function (err, data) {
        if (err) { return next(err) }

        res.send(data)

    })
}
exports.updateSample =  (req, res, next) => {
    const id = req.body._id;

    Sample.findByIdAndUpdate(id,req.body,{new:true},function (err, data) {
            if(err){return next(err);}
            res.send(data)
        })

}