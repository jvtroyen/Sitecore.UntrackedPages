# Untracked pages #

## What ##

We do not want certain pages to be tracked. We are not interested in their usage and tracking them only clutters the Experience Database with unneeded data.
For instance :

- api-calls to anyithing under /api/*

### Why ###

This feature was introduced in Sitecore 8.2, but it does not work with wildcards.
It would be impossible to maintin a list of api-controllers, so we opted to make something urselves.

## Compatibility ##

The module was created and tested on Sitecore 8.1 update-3.

## Usage ##

### Installation ###

The module is made available on the Sitecore marketplace as a Sitecore package. The package includes:

- a config file that includes a pipeline processor and a default list of untracked pages
- the dll
 
## History ##
- v1.0 : initial release
