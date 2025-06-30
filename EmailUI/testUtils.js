import React from 'react';
import { render } from '@testing-library/react';

export const renderWithProviders = (ui) => {
  const { EmailProvider } = require('./src/contexts/EmailContext');
  const { ThemeProvider } = require('./src/contexts/ThemeContext');
  return render(
    <ThemeProvider>
      <EmailProvider>{ui}</EmailProvider>
    </ThemeProvider>
  );
};
