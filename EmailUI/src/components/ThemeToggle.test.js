import { fireEvent } from '@testing-library/react';
import ThemeToggle from './ThemeToggle';
import { renderWithProviders } from '../../testUtils';
import axios from 'axios';

jest.mock('axios', () => ({
  create: () => ({ post: jest.fn() })
}));

// Helper to render component with context
const setup = () => renderWithProviders(<ThemeToggle />);

test('toggles theme when clicked', () => {
  const { getByRole } = setup();
  const button = getByRole('button');
  expect(button).toHaveTextContent('🌙');
  fireEvent.click(button);
  expect(button).toHaveTextContent('☀️');
  expect(localStorage.getItem('theme')).toBe('dark');
});
