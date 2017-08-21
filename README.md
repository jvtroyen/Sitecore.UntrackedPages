# Untracked pages #

## What ##

Expanding on Sitecore OOTB.
We do not want certain pages to be tracked. We are not interested in their usage and tracking them only clutters the Experience Database with unneeded data.
For instance :

- api-calls to anything under /api/*

## Why ##

This feature was introduced in Sitecore 8.2, but we want it available in 8.1 AND it does not work with wildcards.
It would be impossible to maintain a list of api-controllers, and Sitecore-Support suggested we make a modified version ourselves.

## Compatibility ##

The module was created and tested on Sitecore 8.1 update-3.

## Usage ##

### Installation ###

The module is made available on the Sitecore marketplace as a Sitecore package. The package includes:

- a config file that includes a pipeline processor and a default list of untracked pages
- the dll
 
## History ##
- v1.0 : initial release
