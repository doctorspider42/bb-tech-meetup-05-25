// Theme constants for consistent styling across components
const lightTheme = {
  colors: {
    primary: '#4299e1',
    primaryDark: '#3182ce',
    primaryLight: '#90cdf4',
    secondary: '#718096',
    background: '#f5f7fa',
    surface: '#ffffff',
    text: '#2d3748',
    textLight: '#a0aec0',
    textSecondary: '#718096',
    resultText: '#2d3748', // Adding dedicated result text color for consistency
    error: '#e53e3e',
    success: '#38a169',
    warning: '#dd6b20',
    info: '#3182ce',
    border: '#e2e8f0',
    skeleton: '#e2e8f0'
  },
  shadows: {
    small: '0 1px 3px rgba(0, 0, 0, 0.1)',
    medium: '0 4px 6px rgba(0, 0, 0, 0.05)',
    large: '0 10px 25px rgba(0, 0, 0, 0.1)'
  },
  borderRadius: {
    small: '4px',
    medium: '8px',
    large: '12px'
  },
  spacing: {
    xs: '0.25rem', // 4px
    sm: '0.5rem',  // 8px
    md: '1rem',    // 16px
    lg: '1.5rem',  // 24px
    xl: '2rem',    // 32px
    xxl: '3rem'    // 48px
  },
  typography: {
    fontFamily: "'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif",
    fontSize: {
      xs: '0.75rem',  // 12px
      sm: '0.875rem', // 14px
      md: '1rem',     // 16px
      lg: '1.25rem',  // 20px
      xl: '1.5rem',   // 24px
      xxl: '2rem',    // 32px
      xxxl: '2.5rem'  // 40px
    },
    lineHeight: {
      tight: 1.2,
      normal: 1.5,
      relaxed: 1.75
    },
    fontWeight: {
      regular: 400,
      medium: 500,
      semiBold: 600,
      bold: 700
    }
  },
  breakpoints: {
    sm: '576px',
    md: '768px',
    lg: '992px',
    xl: '1200px'
  },
  transitions: {
    fast: '0.1s',
    normal: '0.2s',
    slow: '0.3s'
  }
};

// Dark theme variant
const darkTheme = {
  colors: {
    primary: '#63b3ed',
    primaryDark: '#4299e1',
    primaryLight: '#a0aec0',
    secondary: '#a0aec0',
    background: '#1a202c',
    surface: '#2d3748',
    text: '#f7fafc',
    textLight: '#e2e8f0',
    textSecondary: '#a0aec0',
    resultText: '#ffffff', // Adding dedicated result text color with high contrast
    error: '#fc8181',
    success: '#68d391',
    warning: '#f6ad55',
    info: '#63b3ed',
    border: '#4a5568',
    skeleton: '#4a5568'
  },
  shadows: {
    small: '0 1px 3px rgba(0, 0, 0, 0.2)',
    medium: '0 4px 6px rgba(0, 0, 0, 0.15)',
    large: '0 10px 25px rgba(0, 0, 0, 0.2)'
  },
  borderRadius: lightTheme.borderRadius,
  spacing: lightTheme.spacing,
  typography: lightTheme.typography,
  breakpoints: lightTheme.breakpoints,
  transitions: lightTheme.transitions
};

const themes = {
  light: lightTheme,
  dark: darkTheme
};

export { themes };
export default lightTheme;
