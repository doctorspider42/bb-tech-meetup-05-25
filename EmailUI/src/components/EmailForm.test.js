import { render, fireEvent } from '@testing-library/react';
import axios from 'axios';
import EmailForm from './EmailForm';
import { EmailProvider } from '../contexts/EmailContext';
import { ThemeProvider } from '../contexts/ThemeContext';

jest.mock('axios', () => ({
  create: () => ({ post: jest.fn() })
}));

const setup = () =>
  render(
    <ThemeProvider>
      <EmailProvider>
        <EmailForm />
      </EmailProvider>
    </ThemeProvider>
  );

test('submit button disabled without input', () => {
  const { getByRole, getByLabelText } = setup();
  const button = getByRole('button', { name: /analyze email/i });
  expect(button).toBeDisabled();
  fireEvent.change(getByLabelText(/email content/i), {
    target: { value: 'Hello' },
  });
  expect(button).not.toBeDisabled();
});
