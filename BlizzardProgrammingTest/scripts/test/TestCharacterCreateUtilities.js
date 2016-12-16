var chai = require('chai');
var expect = chai.expect;
var should = chai.should();
var sinon = require('sinon');

//MIN_CLASS_PER_COL, CLASS_COLUMNS, DEFAULT_CLASS
characterApp = { controller: function () { } };
var utilFactory = require('../controllers/characterCreate');

function initClassList(members, defaultValue) {
    var baseClassArray = [];

    for (var i = 1; i < members; i++) {
        baseClassArray.push('Class ' + i);
    }
    baseClassArray.push(defaultValue);

    return baseClassArray;
}

describe('Testing Columnize', function () {
    var util = null;
    var defaultClass = 'default class';
    var baseClassArray = null;
    var MIN_CLASS_PER_COL = 3;
    var CLASS_COLUMNS = 4;

    // Initialize the utility library and the base class array
    beforeEach(function () {
        util = utilFactory(MIN_CLASS_PER_COL, CLASS_COLUMNS, defaultClass);
    });

    describe('Columnize short list', function () {
        it('classList length = 1', function () {
            baseClassArray = initClassList(1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(1);
            columnizedClassArray[0].should.have.lengthOf(1);
        });

        it('classList length = MIN_CLASS_PER_COL - 1', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL - 1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(1);
            columnizedClassArray[0].should.have.lengthOf(2);
        });

        it('classList length = MIN_CLASS_PER_COL', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(1);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
        });

        it('classList length = MIN_CLASS_PER_COL + 1', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL + 1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(2);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[1].should.have.lengthOf(1);
        });

        it('classList length = MIN_CLASS_PER_COL * 2 - 1', function () {
            baseClassArray = initClassList((MIN_CLASS_PER_COL * 2) - 1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(2);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[1].should.have.lengthOf(2);
        });

        it('classList length = MIN_CLASS_PER_COL * 2', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL * 2, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(2);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[1].should.have.lengthOf(MIN_CLASS_PER_COL);
        });

        it('classList length = MIN_CLASS_PER_COL * CLASS_COLUMNS - 1', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL * CLASS_COLUMNS - 1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(CLASS_COLUMNS);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[1].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[2].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[3].should.have.lengthOf(MIN_CLASS_PER_COL - 1);
        });

        it('classList length = MIN_CLASS_PER_COL * CLASS_COLUMNS', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL * CLASS_COLUMNS, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(CLASS_COLUMNS);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[1].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[2].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[3].should.have.lengthOf(MIN_CLASS_PER_COL);
        });

        it('classList length = MIN_CLASS_PER_COL * CLASS_COLUMNS + 1', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL * CLASS_COLUMNS + 1, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(CLASS_COLUMNS);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL + 1);
            columnizedClassArray[1].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[2].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[3].should.have.lengthOf(MIN_CLASS_PER_COL);
        });

        it('classList length = MIN_CLASS_PER_COL * CLASS_COLUMNS + 2', function () {
            baseClassArray = initClassList(MIN_CLASS_PER_COL * CLASS_COLUMNS + 2, defaultClass);

            var columnizedClassArray = util.columnizeClasses(baseClassArray);

            expect(columnizedClassArray).to.be.an('Array');
            columnizedClassArray.should.have.lengthOf(CLASS_COLUMNS);
            columnizedClassArray[0].should.have.lengthOf(MIN_CLASS_PER_COL + 1);
            columnizedClassArray[1].should.have.lengthOf(MIN_CLASS_PER_COL + 1);
            columnizedClassArray[2].should.have.lengthOf(MIN_CLASS_PER_COL);
            columnizedClassArray[3].should.have.lengthOf(MIN_CLASS_PER_COL);
        });
    });
})