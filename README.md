# VendingMachine
Simple web application which simulates vending machine behavior.

## Table of contents
* [About](#about)
* [Features](#features)
* [Basic usage](#basic-usage)

## About
This project simulates vending machine behavior. It contains 2 parts - client part (insert coins - buy drinks - get change) and admin part (edit drinks, add/delete drinks, edit coins).
Client part is opened by default. To access admin part you should add the ```secretKey=thisissupersecrettoken``` query parameter. For example, if your app runs on ```https://localhost:1234/```, then you can reach admin part via ```https://localhost:1234/?secretKey=thisissupersecrettoken```

## Features
This app has some features including:
* Local DB
* Service injection via ASP .NET DI container
* Change is displayed on screen when "Get change" button is pushed (greedy change algorithm is used)
* Project is styled via Bootstrap 5

## Basic usage
To use this project you need to perform the following steps:
* Clone the project
```bash
git clone https://github.com/Slepoyi/VendingMachine.git
```
* Step into directory of the solution
```bash
cd VendingMachine
```
* Build the project
```bash
dotnet build VendingMachine.sln
```
* Step into directory of the solution:
```bash
cd src
cd VendingMachine.UI
```
* Run the project
```bash
dotnet run
```
