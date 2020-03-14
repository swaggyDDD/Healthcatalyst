const   gulp = require('gulp'),
        jshint = require('gulp-jshint'),
        rimraf = require('rimraf'),
        plugins = require('gulp-load-plugins')({
            lazy: true
        }),
        concat = require('gulp-concat'),
        rename = require('gulp-rename'),
        uglify = require('gulp-uglify'),
        runSequence = require('run-sequence');

//// PATHS
const   paths = {
            src: './src/',
            peeps: './src/peeps/',
            dist: './dist/',
            catalyst: '../Catalyst.Web/scripts/lib/catalyst/'
        };

// cleans the local distribution directory
gulp.task('clean:dist', function(done) {
    rimraf(paths.dist, done);
});

// cleans the visual studio directory
gulp.task('clean:vs', function(done) {
    rimraf(paths.catalyst, done);
});

// assembles scripts - concatenation and minification
gulp.task('assemble', function() {
    return gulp.src([ paths.peeps + 'Peeps.js', paths.peeps + 'Peeps.Settings.js', paths.peeps + 'modules/**/*.js', paths.peeps + 'editors/**/*.js', paths.peeps + 'bootstrap.js' ])
        .pipe(concat('peeps.js'))
        .pipe(gulp.dest(paths.dist))
        .pipe(rename('peeps.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(paths.dist));
});

gulp.task('move:vs', function(done) {
    return gulp.src(paths.dist + '*.js')
        .pipe(gulp.dest(paths.catalyst));
});

// default task
gulp.task('default', function(done) {
    runSequence('clean:vs', 'clean:dist', 'assemble', 'move:vs', done);
});
