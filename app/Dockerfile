FROM node as publisher

WORKDIR /publish

COPY package.json .

RUN yarn install

COPY . .

RUN yarn build

FROM node as published

RUN npm install -g serve

WORKDIR /app

COPY --from=publisher /publish/build .

ENTRYPOINT ["serve", "-s", ".", "-p", "80"]