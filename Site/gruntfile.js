module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        uglify: {
            my_target: {
                files: {
                    'www/js/app.js': [
                        'scripts/angular/angular.js', 'scripts/angular/angular-route.js', 'scripts/angular/angular-locale_ru-ru.js',
                        'scripts/chart/moment.js', 'scripts/chart/chart.js', 'scripts/chart/angular-chart.js',
                        'scripts/*.js',
                        'app/app.js', 'app/**/*.js', 'app/pages/**/*.js']
                }
            }
        },
        
        cssmin: {
            options: {
                shorthandCompacting: false,
                roundingPrecision: -1
            },
            target: {
                files: {
                    'www/css/site.css': ['content/css/*.css']
                }
            }
        },

        html2js: {
            options: {
                htmlmin: {
                    removeComments: true,
                    collapseWhitespace: true,
                    keepClosingSlash: true
                }
            },
            main: {
                src: ['app/pages/**/*.html', 'app/directives/*.html'],
                dest: 'www/js/templates.js'
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-html2js');

    grunt.registerTask('default', ['uglify', 'cssmin', 'html2js']);
};