import React, { createContext, useState, useContext, useEffect } from 'react';
import { themes } from '../theme';

const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
  // Check if there's a saved preference in localStorage
  const getSavedTheme = () => {
    const savedTheme = localStorage.getItem('theme');
    return savedTheme && themes[savedTheme] ? savedTheme : 'light';
  };

  const [currentTheme, setCurrentTheme] = useState(getSavedTheme());
  
  // Function to toggle between light and dark themes
  const toggleTheme = () => {
    const newTheme = currentTheme === 'light' ? 'dark' : 'light';
    setCurrentTheme(newTheme);
    localStorage.setItem('theme', newTheme);
  };

  // Set the data-theme attribute on the document when theme changes
  useEffect(() => {
    document.documentElement.setAttribute('data-theme', currentTheme);
  }, [currentTheme]);

  return (
    <ThemeContext.Provider value={{ 
      theme: themes[currentTheme], 
      currentTheme,
      toggleTheme 
    }}>
      {children}
    </ThemeContext.Provider>
  );
};

export const useTheme = () => useContext(ThemeContext);
