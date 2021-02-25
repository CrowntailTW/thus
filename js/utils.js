/* global Chart */

'use strict';

window.chartColors = {
    Target2red: 'rgba(255, 50, 0,0.7)',
    Target1red: 'rgba(255, 25, 0,1)',
    Targetgreen: 'rgba(255, 25, 0,1)',
    
    red: 'rgba(255, 99, 132,0.6)',
	orange: 'rgba(255, 159, 64,0.6)',
	yellow: 'rgba(255, 205, 86,0.6)',
	green: 'rgba(75, 192, 192,0.6)',
	blue: 'rgba(54, 162, 235,0.6)',
	purple: 'rgba(153, 102, 255,0.6)',
	grey: 'rgba(201, 203, 207,0.6)',
	yellowgreen: 'rgba(200, 192, 86,0.6)',

	press_AX: 'rgba(50,200,0,0.7)',
	press_AY: 'rgba(0,150,150,0.7)',
	press_BX: 'rgba(118,200,0,0.7)',
	press_BY: 'rgba(50,150,150,0.7)',
    /*
    redA: 'rgba(255, 99, 132,1)',
    orangeA: 'rgba(255, 159, 64,1)',
    yellowA: 'rgba(255, 205, 86,1)',
    greenA: 'rgba(75, 192, 192,1)',
    blueA: 'rgba(54, 162, 235,1)',
    purpleA: 'rgba(153, 102, 255,1)',
    greyA: 'rgba(201, 203, 207,1)',
    yellowgreenA: 'rgba(200, 192, 86,1)'
    */

	redA: 'rgba(255, 110, 142,1)',
	orangeA: 'rgba(255, 170, 75,1)',
	yellowA: 'rgba(255, 215, 96,1)',
	greenA: 'rgba(85, 202, 202,1)',
	blueA: 'rgba(64, 172, 245,1)',
	purpleA: 'rgba(163, 112, 265,1)',
	greyA: 'rgba(211, 213, 217,1)',
	yellowgreenA: 'rgba(210, 202, 96,1)'
    };
/*
window.chartColorsA = {
    red: 'rgba(255, 99, 132,1)',
    orange: 'rgba(255, 159, 64,1)',
    yellow: 'rgba(255, 205, 86,1)',
    green: 'rgba(75, 192, 192,1)',
    blue: 'rgba(54, 162, 235,1)',
    purple: 'rgba(153, 102, 255,1)',
    grey: 'rgba(201, 203, 207,1)',
    yellowgreen: 'rgba(200, 192, 86,1)'
};

window.chartColorsA = {
    red: 'rgb(230, 89, 92)',
    orange: 'rgb(230, 149, 44)',
    yellow: 'rgb(230, 195, 66)',
    green: 'rgb(50, 182, 172)',
    blue: 'rgb(29, 152, 215)',
    purple: 'rgb(128, 92, 235)',
    grey: 'rgb(176, 193, 187)',
    yellowgreen: 'rgb(175, 182, 66)'
};*/

window.randomScalingFactor = function() {
	return (Math.random() > 0.5 ? 1.0 : -1.0) * Math.round(Math.random() * 100);
};

(function(global) {
	var Months = [
		'January',
		'February',
		'March',
		'April',
		'May',
		'June',
		'July',
		'August',
		'September',
		'October',
		'November',
		'December'
	];

	var Samples = global.Samples || (global.Samples = {});
	Samples.utils = {
		// Adapted from http://indiegamr.com/generate-repeatable-random-numbers-in-js/
		srand: function(seed) {
			this._seed = seed;
		},

		rand: function(min, max) {
			var seed = this._seed;
			min = min === undefined? 0 : min;
			max = max === undefined? 1 : max;
			this._seed = (seed * 9301 + 49297) % 233280;
			return min + (this._seed / 233280) * (max - min);
		},

		numbers: function(config) {
			var cfg = config || {};
			var min = cfg.min || 0;
			var max = cfg.max || 1;
			var from = cfg.from || [];
			var count = cfg.count || 8;
			var decimals = cfg.decimals || 8;
			var continuity = cfg.continuity || 1;
			var dfactor = Math.pow(10, decimals) || 0;
			var data = [];
			var i, value;

			for (i=0; i<count; ++i) {
				value = (from[i] || 0) + this.rand(min, max);
				if (this.rand() <= continuity) {
					data.push(Math.round(dfactor * value) / dfactor);
				} else {
					data.push(null);
				}
			}

			return data;
		},

		labels: function(config) {
			var cfg = config || {};
			var min = cfg.min || 0;
			var max = cfg.max || 100;
			var count = cfg.count || 8;
			var step = (max-min) / count;
			var decimals = cfg.decimals || 8;
			var dfactor = Math.pow(10, decimals) || 0;
			var prefix = cfg.prefix || '';
			var values = [];
			var i;

			for (i=min; i<max; i+=step) {
				values.push(prefix + Math.round(dfactor * i) / dfactor);
			}

			return values;
		},

		months: function(config) {
			var cfg = config || {};
			var count = cfg.count || 12;
			var section = cfg.section;
			var values = [];
			var i, value;

			for (i=0; i<count; ++i) {
				value = Months[Math.ceil(i)%12];
				values.push(value.substring(0, section));
			}

			return values;
		},

		transparentize: function(color, opacity) {
			var alpha = opacity === undefined? 0.5 : 1 - opacity;
			return Chart.helpers.color(color).alpha(alpha).rgbString();
		},

		merge: Chart.helpers.configMerge
	};

	Samples.utils.srand(Date.now());

	// Google Analytics
	if (document.location.hostname === 'www.chartjs.org') {
		(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
		(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
		m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
		})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
		ga('create', 'UA-28909194-3', 'auto');
		ga('send', 'pageview');
	}

}(this));

