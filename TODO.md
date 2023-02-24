#  TODO List

## New Appliction Feature
- [ ] Polly
- [ ] System.Runtime.Caching
- [ ] LiveCharts

## Design Principle
- [ ] Consider replacing `Kind` types with `record` definition and add global namespace definition in the module definition file  
- [ ] The kernel module only contain data structure definition,data initialization and alteration with the eventservice in the service module
- [ ] The class constructed function only contain IOC operation,the eventservice publish or subscribe operation move to the `Loaded` function  
- [ ] The application need infrastructure that manage the message contain `INFO` and `ERROR` level,the `ERROR` level prevent the application to run  