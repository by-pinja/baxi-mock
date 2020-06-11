# baxi-mock
A simple Nets Connect@Cloud mock software to ease up debugging of Nets BAXI problems

## how does it work?
Just like a production baxi: Create connection with Payment service and start making payment. Do make sure that the weboscket server address
is configured in payment service.

## how can I simulate successful payment?
Start making payment while this is running on background. After payment is waiting for response,
console will ask from you whether to set payment as success or as fail. Press respective button and Voila!
