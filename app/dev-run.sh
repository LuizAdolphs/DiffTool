#!/bin/bash

docker run -it --rm -p 3000:3000 -v $PWD:/app -w /app node /bin/bash