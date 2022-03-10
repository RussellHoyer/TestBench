# Test Bench

The Test Bench is a playground meant to house all those annoying little questions you have (and forgot) about code and functions. This all started when I found myself forgetting things like "wait, was it Count or Length that I need to use... and which one needs the off by 1 correction again?". Since then it has grown into a much larger project encompassing parts of my never-ending journey to learn more about progamming concepts and principles.

This project was started many years ago and grew into what it is now... coming soon, an upgrade to .Net 6 (or maybe 7 depending on how fast that gets released).

## Usage

The way to use the Test Bench is "relatively straight-forward(tm)". Find a class in the Tests folder (or create your own) and implement the ITest interface, which has one public void method that will be used to execute any/all of the other methods you write.

**Update**: I recently implemented the "TestableTemplate" class which implements the ITest interface. It uses reflection to get all of the public void methods inside the class and run them dynamically. There are some performance considerations to be had here, but given this is a playground style project that is essentially perpetually in development, these concerns are generally out of scope. 

With the addition of the TestableTemplate, I also added the ability to limit the number of tests via command line parameters. While the overall number of tests (as of this writing) shouldn't overwhelm any modern system--I thought it would be a good way to both counter the potential performance impact, as well as limit the tests being run to a specific sample size (this helps with reading the output of each test).

To use the command line features, add the Class name of the test(s) you want to run, multiple can be selected by using spaces:
```
Project Properties -> Debug -> Command line arguments:
AssemblyTests StringTests
```

_Note: If no command line arguments are present then the code will run all of the public classes that implement ITest._

## Troubleshooting
If it's broke... figure out why and fix it. It's a sandbox.

Once you've fixed it, see if you can break it again in fun and entertaining ways. It's all about learning.

## About
As a self-taught developer I've always learned best through trial and error and this is the epitome of that mindset translated in code.