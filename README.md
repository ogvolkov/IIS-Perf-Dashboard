IIS-Perf-Dashboard
==================

Quick and dirty IIS log analyzer serving as a web app performance dashboard

Usage: change LogsPath setting in Web.config to the path to directory where IIS logs are stored and start the app.

By default, only logs for today are analyzed. That can be easily changed in HomeController.Index().