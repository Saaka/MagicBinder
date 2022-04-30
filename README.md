# MagicBinder [![Build Status](https://app.travis-ci.com/Saaka/MagicBinder.svg?branch=main)](https://app.travis-ci.com/Saaka/MagicBinder)

MagicBinder is website that lets users manage their MTG card collection, create decks and search for cards from entire cards database.

##Setting up project
Project contains of two main parts: 
* Backend solution written in [.NET6](https://docs.microsoft.com/dotnet) using [MongoDB](https://www.mongodb.com/docs/) for storing data
* Website using [React](https://reactjs.org/) JavaScript framework and [Bulma](https://bulma.io/) CSS framework

###Backend solution appsettings.json
Create `appsettings.{environment}.json` file for given environment (for example `appsettings.Development.json`) and add all required values found in base `appsetting.json` file.
* Mongo
  * ConnectionString - your MongoDB server address
  * Database - name of MongoDB database where all core application data will be stored (*default value: MagicBinderCards*)
* Hangfire - settings for [Hangfire](https://www.hangfire.io/), currently not used, but recurring jobs will be handled by this library
  * DashboardEnabled - determine if dashboard for hangfire should be enabled
  * DatabaseName - name of MongoDB database where hangfire data will be stored (*default value: HangfireMagicBinder*)
  * RetryCount - number of retries for jobs
* Auth - project is integrated with [IdentityIssuer](https://github.com/Saaka/IdentityIssuer), my other project that is responsible for storing data of users. All settings here should match your IdentityIssuer settings.
  * Secret - secret key shared  with IdentityIssuer, used for token encoding
  * Issuer - name of issuer 
  * AppCode - tenant code in IdentityIssuer
  * IdentityIssuerUrl - url of IdentityIssuer backend
  * AllowedOrigin - url of your frontend MagicBinder application
