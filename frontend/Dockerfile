# Use a Node image to install dependencies and build the React app.
FROM node:24-alpine AS build

# Set the working folder inside the frontend build container.
WORKDIR /app

# Copy package files first so Docker can cache npm install steps.
COPY package*.json ./

# Install exact dependency versions from package-lock.json for repeatable builds.
RUN npm ci

# Copy the rest of the React source code into the container.
COPY . .

# Define the backend API URL used by Vite at build time.
ARG VITE_API_BASE_URL=http://localhost:5217

# Expose the build argument as an environment variable for the Vite build.
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL

# Build static HTML, CSS, and JavaScript files into the dist folder.
RUN npm run build

# Use Nginx to serve the compiled React files in a lightweight web server.
FROM nginx:1.27-alpine AS runtime

# Copy a simple Nginx config that supports React fallback routing.
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy built React files from the build stage into Nginx's public folder.
COPY --from=build /app/dist /usr/share/nginx/html

# Tell Docker that Nginx listens on port 80 inside the container.
EXPOSE 80

# Start Nginx in the foreground so Docker can manage the process.
CMD ["nginx", "-g", "daemon off;"]
