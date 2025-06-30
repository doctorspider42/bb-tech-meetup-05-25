import { render, fireEvent } from '@testing-library/react';
import ConfirmationDialog from './ConfirmationDialog';

const renderDialog = (props = {}) =>
  render(
    <ConfirmationDialog
      title="Confirm"
      message="Are you sure?"
      onClose={props.onClose || jest.fn()}
      onConfirm={props.onConfirm || jest.fn()}
    />
  );

test('confirm button triggers callbacks', () => {
  const onConfirm = jest.fn();
  const onClose = jest.fn();
  const { getByRole } = renderDialog({ onConfirm, onClose });
  fireEvent.click(getByRole('button', { name: /continue/i }));
  expect(onConfirm).toHaveBeenCalledTimes(1);
  expect(onClose).toHaveBeenCalledTimes(1);
});

test('renders provided title and message', () => {
  const { getByText } = renderDialog();
  expect(getByText('Confirm')).toBeInTheDocument();
  expect(getByText('Are you sure?')).toBeInTheDocument();
});

test('cancel button only closes dialog', () => {
  const onConfirm = jest.fn();
  const onClose = jest.fn();
  const { getByRole } = renderDialog({ onConfirm, onClose });
  fireEvent.click(getByRole('button', { name: /cancel/i }));
  expect(onConfirm).not.toHaveBeenCalled();
  expect(onClose).toHaveBeenCalledTimes(1);
});
