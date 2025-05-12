import React from 'react';
import { useTheme } from '../contexts/ThemeContext';

const ThemeToggle = () => {
  const { currentTheme, toggleTheme } = useTheme();

  return (
    <button 
      onClick={toggleTheme}
      style={{
        position: 'fixed',
        top: '1rem',
        right: '1rem',
        padding: '0.5rem',
        background: 'transparent',
        border: 'none',
        cursor: 'pointer',
        zIndex: 1000,
        fontSize: '1.5rem'
      }}
      aria-label={`Switch to ${currentTheme === 'light' ? 'dark' : 'light'} theme`}
      title={`Switch to ${currentTheme === 'light' ? 'dark' : 'light'} theme`}
    >
      {currentTheme === 'light' ? '🌙' : '☀️'}
    </button>
  );
};

export default ThemeToggle;
