# Options Scanner [Work In Progress]

Implementation of IB API that allows to find most profitable options by selected criteria.
UI allows to download options for selected symbols and dates, save them to DB, and search for preferable option or their combination in DB.

# Layers

- Angular 6 
- Node JS 
- TWS API or Mongo DB
- Interactive Brokers 

# Requirements

Everything is implemented in JS, so all working files are in the project IBScanner.
Implementation in C# MVC is in progress.

- Mongo DB needs to be started on port 6000
- Install dependencies for Angular and Node projects preforming "npm i" from IBScanner and IBScanner/server folders
- Angular UI should be started from the root of IBScanner with a command "ng serve"
- Node JS server should be started from IBScanner/server with a command "ts-node server"

UI should be available at http://localhost:4000

[Preview](https://i.imgur.com/YH28Ckf.png)
