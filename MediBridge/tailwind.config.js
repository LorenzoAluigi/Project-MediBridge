/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}", "./node_modules/flowbite/**/*.js"],
  theme: {
    extend: {
      colors: {
        primary: "#090d30",
        secondary: "#2a4ae1",
        accent: "#da00f9",
      },
      darkMode: "class",
    },
    fontFamily: {
      sans: ["Poppins", "sans-serif"],
      serif: ["Merriweather", "serif"],
    },
  },
  plugins: [require("flowbite/plugin")],
};
