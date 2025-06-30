import { render, fireEvent } from '@testing-library/react';
import ThemeToggle from './ThemeToggle';
import { ThemeProvider } from '../contexts/ThemeContext';

// Helper to render component with context
const setup = () => {
  return render(
    <ThemeProvider>
      <ThemeToggle />
    </ThemeProvider>
  );
};

test('toggles theme when clicked', () => {
  const { getByRole } = setup();
  const button = getByRole('button');
  expect(button).toHaveTextContent('🌙'); // initial light theme shows moon
  fireEvent.click(button);
  expect(button).toHaveTextContent('☀️'); // after toggle shows sun
});
